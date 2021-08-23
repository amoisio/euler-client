using System.Threading.Tasks;

namespace ProjectEuler.Client
{
    /// <summary>
    /// Data to which the project euler HTML pages are parsed (stripped of HTML)
    /// </summary>
    public class ProblemDetails
    {
        internal ProblemDetails(string fileRef, int number, string title, string detailsHtml)
        {
            Ref = fileRef;
            Number = number;
            Title = title;
            DetailsHtml = detailsHtml;
        }

        /// <summary>
        /// Problem unique reference, such as the web uri where the problem may be found.
        /// </summary>
        public string Ref { get; }

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
