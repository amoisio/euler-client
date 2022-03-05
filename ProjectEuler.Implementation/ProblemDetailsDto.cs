namespace ProjectEuler.Implementation
{
  class ProblemDetailsDto
  {
    internal ProblemDetailsDto(string fileRef, int number, string title, string detailsHtml)
    {
      Ref = fileRef;
      Number = number;
      Title = title;
      DescriptionHtml = detailsHtml;
    }

    public string Ref { get; }

    public int Number { get; }

    public string Title { get; }

    public string DescriptionHtml { get; }
  }
}
