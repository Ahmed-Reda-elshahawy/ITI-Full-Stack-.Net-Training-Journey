using observer.Models;

namespace observer.Services.Offers;

class OfferCreatedEventArgs:EventArgs
{
    public OfferCreatedEventArgs(Offer offer)
    {
        Offer = offer;
    }

    public Offer Offer { get; }
}
