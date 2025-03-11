import { ILocation } from './ILocation';

export interface IMappable {
  location: ILocation;
  markerContent(): string;
}
