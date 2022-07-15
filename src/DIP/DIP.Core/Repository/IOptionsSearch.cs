namespace DIP.Core.Repository
{
    public interface IOptionsSearch
    {
        int? RegisterPerPage { get; set; }
        int? Page { get; set; }

        void Validate();
    }
}
