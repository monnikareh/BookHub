using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthorService _authorService;
    private readonly IBookService _bookService;
    private readonly IGenreService _genreService;
    private readonly IPublisherService _publisherService;
    private readonly IRatingService _ratingService;

    public HomeController(ILogger<HomeController> logger, IAuthorService authorService, IBookService bookService,
        IGenreService genreService, IPublisherService publisherService, IRatingService ratingService)
    {
        _logger = logger;
        _authorService = authorService;
        _bookService = bookService;
        _genreService = genreService;
        _publisherService = publisherService;
        _ratingService = ratingService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Search(string? query)
    {
        var authors = await _authorService.GetSearchAuthorsAsync(query);
        var books = await _bookService.GetSearchBooksAsync(null, query);
        var genres = await _genreService.GetSearchGenresAsync(query);
        var publishers = await _publisherService.GetSearchPublishersAsync(query);
        var ratings = await _ratingService.GetSearchRatingsAsync(query);
        return View(new SearchType(authors, books, genres, publishers, ratings));
    }
}