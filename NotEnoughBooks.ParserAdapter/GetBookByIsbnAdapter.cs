using System.Net.Http.Json;
using System.Text.RegularExpressions;
using ConstructorGenerator.Attributes;
using HtmlAgilityPack;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Parser.DNB;

[GenerateFullConstructor]
public partial class GetBookByIsbnAdapter : IGetBookByIsbnPort
{
    private const string DNB_URL = "https://portal.dnb.de/opac.htm?method=simpleSearch&query=";
    private const string GOOGLE_BOOKS_URL = "https://www.googleapis.com/books/v1/volumes?q=isbn:";
    private const string DNB_COVER_URL = "https://portal.dnb.de/opac/mvb/cover?isbn=";

    private readonly HttpClient _httpClient;

    public async Task<BookResult> GetBook(string isbn)
    {
        BookResult bookResult = await GetBookByQuery(isbn);
        if (!bookResult.Success)
            return bookResult;

        if (bookResult.Book.Price == 0)
        {
            bookResult.Book.Price = await GetBookPriceFromDnb(bookResult.Book.Isbn);
        }

        return bookResult;
    }

    private async Task<BookResult> GetBookByQuery(string isbn)
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync(GOOGLE_BOOKS_URL + isbn);
        if (!responseMessage.IsSuccessStatusCode)
            return BookResult.Create(responseMessage.ReasonPhrase);

        GoogleBooksResult responseObject = await responseMessage.Content.ReadFromJsonAsync<GoogleBooksResult>();

        if (responseObject.TotalItems == 0)
            return BookResult.Create("No Books Found");
        
        GoogleVolumeInfo responseObjectItem = responseObject.Items[0].VolumeInfo;
        
        DateOnly publishedDate = responseObjectItem.PublishedDate.Length != 4 ? DateOnly.Parse(responseObjectItem.PublishedDate) : new DateOnly(int.Parse(responseObjectItem.PublishedDate), 1, 1);
        
        Book result = new Book()
        {
            Isbn = responseObjectItem.IndustryIdentifiers.First(x => x.Type == "ISBN_13").Identifier,
            Title = responseObjectItem.Title,
            Authors = string.Join(", ", responseObjectItem.Authors),
            Published = publishedDate,
            Description = responseObjectItem.Description,
            PageCount = responseObjectItem.PageCount,
            Subtitle = responseObjectItem.Subtitle,
            Publisher = responseObjectItem.Publisher
        };
        result.ImagePath = responseObjectItem.ImageLinks?.Thumbnail ?? DNB_COVER_URL+result.Isbn;
        
        return BookResult.Create(result);
    }

    private async Task<double> GetBookPriceFromDnb(string query)
    {
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(DNB_URL + query);
        if (!httpResponseMessage.IsSuccessStatusCode) throw new HttpRequestException(httpResponseMessage.ReasonPhrase);

        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(await httpResponseMessage.Content.ReadAsStringAsync());

        HtmlNodeCollection htmlTable = document.DocumentNode.SelectNodes("//table[@id='fullRecordTable']//tr");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        // Fucking lies
        if (htmlTable == null)
            return 0;

        foreach (HtmlNode htmlTableRow in htmlTable)
        {
            HtmlNode firstCell = htmlTableRow.SelectSingleNode("td");
            // ReSharper disable once ConstantConditionalAccessQualifier
            // ITS A LIE
            if (string.IsNullOrEmpty(firstCell?.InnerText.Trim()))
                continue;

            HtmlNode secondCell = htmlTableRow.SelectSingleNode("td[2]");
            if (firstCell.InnerText.Trim() != "ISBN/Einband/Preis")
                continue;

            MatchCollection priceMatches = PriceRegex().Matches(secondCell.InnerText.Trim());
            string dePrice = priceMatches.FirstOrDefault(match => match.Value.Split(' ')[1] == "(DE)")?.Value.Split(' ')[0] ?? "0";
            return double.Parse(dePrice);
        }

        return 0;
    }

    [GeneratedRegex(@"((\d{1,})\.\d\d \(\w\w\))")]
    private static partial Regex PriceRegex();
    
    // ReSharper disable UnusedAutoPropertyAccessor.Local
    private class GoogleBooksResult
    {
        public int TotalItems { get; set; }
        public GoogleBook[] Items { get; set; }
    }

    private class GoogleBook
    {
        public GoogleVolumeInfo VolumeInfo { get; set; }
    }

    private class GoogleVolumeInfo
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public string[] Authors { get; set; }
        public string PublishedDate { get; set; }
        public GoogleIndustryIdentifier[] IndustryIdentifiers { get; set; }
        public GoogleImageLinks ImageLinks { get; set; }
    }

    private class GoogleIndustryIdentifier
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }

    private class GoogleImageLinks
    {
        public string Thumbnail { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Local
}
