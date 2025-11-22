using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;
using NotEnoughBooks.Data.Models;

namespace NotEnoughBooks;

public partial class Parser
{
    private const string BASE_URL = "https://portal.dnb.de/opac.htm?method=simpleSearch&query=";
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<Book> Parse(string isbn)
    {
        Book result = new Book
        {
            Id = Guid.NewGuid(),
            Isbn = isbn,
            AddedOn = DateTime.Now,
        };
        
        HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(BASE_URL + isbn);
        if (!httpResponseMessage.IsSuccessStatusCode) return result;
        
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(await httpResponseMessage.Content.ReadAsStringAsync());

        HtmlNodeCollection htmlTable = document.DocumentNode.SelectNodes("//table[@id='fullRecordTable']//tr");
        if (htmlTable == null)
            throw new Exception("No table found");
        foreach (HtmlNode htmlTableRow in htmlTable)
        {
            HtmlNode firstCell = htmlTableRow.SelectSingleNode("td");
            if (firstCell?.InnerText.Trim().IsNullOrEmpty() != false)
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
                    MatchCollection matches = PriceRegex().Matches(secondCell.InnerText.Trim());
                    string first = matches.First(match => match.Value.Split(' ')[1] == "(DE)").Value.Split(' ')[0];
                    result.Price = double.Parse(first);
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