import { IMappable } from '../interfaces/IMappable';

export class MapComponent {
  private googleMap: google.maps.Map;

  constructor(mapDiv: HTMLDivElement, zoom: number = 2) {
    this.googleMap = new google.maps.Map(mapDiv, {
      center: { lat: 0, lng: 0 }, // Default center
      zoom,
    });
  }

  setMarker(mappable: IMappable): MapComponent {
    const marker = new google.maps.Marker({
      map: this.googleMap,
      position: mappable.location,
    });

    const infoWindow = new google.maps.InfoWindow({
      content: mappable.markerContent(),
    });
    marker.addListener('click', _ => infoWindow.open(this.googleMap, marker));

    return this;
  }
}
