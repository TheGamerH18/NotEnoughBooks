using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Parser.DNB;

public partial class Parser : IGetBookByQueryPort
{
    private const string BASE_URL = "https://portal.dnb.de/opac.htm?method=simpleSearch&query=";
    private const string COVER_URL = "https://portal.dnb.de/opac/mvb/cover?isbn=";

    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<BookResult> GetBook(string query)
    {
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(BASE_URL + query);
        if (!httpResponseMessage.IsSuccessStatusCode) throw new HttpRequestException(httpResponseMessage.ReasonPhrase);
        
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(await httpResponseMessage.Content.ReadAsStringAsync());

        HtmlNodeCollection htmlTable = document.DocumentNode.SelectNodes("//table[@id='fullRecordTable']//tr");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        // ReSharper disable once HeuristicUnreachableCode
        // Fucking lies
        if (htmlTable == null)
            return BookResult.Create("Book not found");
        
        Book result = ParseBookFromTable(query, htmlTable);
        return BookResult.Create(result);
    }

    private Book ParseBookFromTable(string isbn, HtmlNodeCollection htmlTable)
    {
        Book result = new Book
        {
            Id = Guid.NewGuid(),
            Isbn = isbn,
            AddedOn = DateTime.Now,
            ImagePath = COVER_URL + isbn,
        };

        foreach (HtmlNode htmlTableRow in htmlTable)
        {
            HtmlNode firstCell = htmlTableRow.SelectSingleNode("td");
            // ReSharper disable once ConstantConditionalAccessQualifier
            // ITS A LIE
            if (string.IsNullOrEmpty(firstCell?.InnerText.Trim()))
                continue;
            
            HtmlNode secondCell = htmlTableRow.SelectSingleNode("td[2]");
            switch (firstCell.InnerText.Trim())
            {
                case "Titel":
                    result.Title = secondCell.InnerText.Trim();
                    break;
                case "Person(en)":
                    result.Author = secondCell.InnerText.Trim();
                    break;
                case "ISBN/Einband/Preis":
                    MatchCollection priceMatches = PriceRegex().Matches(secondCell.InnerText.Trim());
                    string dePrice = priceMatches.First(match => match.Value.Split(' ')[1] == "(DE)").Value.Split(' ')[0];
                    result.Price = double.Parse(dePrice);
                    break;
                case "Zeitliche Einordnung":
                    result.PublishedYear = ReleaseYear().Match(secondCell.InnerText.Trim()).Value;
                    break;
            }
        }
        return result;
    }

    [GeneratedRegex(@"((\d{1,})\.\d\d \(\w\w\))")]
    private static partial Regex PriceRegex();

    [GeneratedRegex(@"(\d{4})")]
    private static partial Regex ReleaseYear();
}