using observer.Models;

namespace observer.Services.Offers;

class OffersManagementService
{
    private readonly List<Offer> _offers = [];
    public event EventHandler<OfferCreatedEventArgs> OfferCreated;

    protected virtual void OnOfferCreated(OfferCreatedEventArgs e)
    {
        OfferCreated?.Invoke(this, e);
    }

    public List<Offer> GetAllOffers()
    {
        return _offers;
    }

    public Offer? GetOfferById(int id)
    {
        return _offers.FirstOrDefault(o => o.Id == id);
    }

    public void CreateOffer(Offer offer)
    {
        _offers.Add(offer);
        OnOfferCreated(new OfferCreatedEventArgs(offer));
    }
}
