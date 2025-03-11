import { faker } from '@faker-js/faker';
import { ILocation } from '../interfaces/ILocation';

export class Location implements ILocation {
  lat: number;
  lng: number;

  constructor(lat?: number, lng?: number) {
    this.lat = lat ?? faker.location.latitude();
    this.lng = lng ?? faker.location.longitude();
  }
}
