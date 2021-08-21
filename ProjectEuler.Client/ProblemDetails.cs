namespace ProjectEuler.Client
{
    /// <summary>
    /// Data to which the project euler HTML pages are parsed (stripped of HTML)
    /// </summary>
    public class ProblemDetails
    {
        public ProblemDetails(int number, string title, string detailsHtml)
        {
            Number = number;
            Title = title;
            DetailsHtml = detailsHtml;
        }

        /// <summary>
        /// Number of the problem
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Problem title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Problem details in HTML
        /// </summary>
        public string DetailsHtml { get; }
    }
}
