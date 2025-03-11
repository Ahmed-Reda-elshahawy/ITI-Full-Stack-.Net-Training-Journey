import { MapComponent } from './components/map.component';
import { User } from './models/user';
import { Company } from './models/company';

// DOM Elements
const mapDiv = document.getElementById('map') as HTMLDivElement;

const user = new User();
const company = new Company();

// Initialize MapComponent first
const map = new MapComponent(mapDiv);

// Add multiple markers
map.setMarker(user).setMarker(company);
