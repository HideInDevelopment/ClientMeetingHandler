import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Location, LocationDetail} from '../models/location.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private apiUrl = `${environment.apiUrl}/location`;

  constructor(private http: HttpClient) { }

  getAllLocations(): Observable<Location[]> {
    return this.http.get<Location[]>(`${this.apiUrl}/simple`);
  }

  getLocationById(id: string): Observable<Location> {
    return this.http.get<Location>(`${this.apiUrl}/simple/${id}`);
  }

  getAllLocationDetails(): Observable<LocationDetail[]> {
    return this.http.get<LocationDetail[]>(`${this.apiUrl}/detail`);
  }

  getLocationDetail(id: string): Observable<LocationDetail> {
    return this.http.get<LocationDetail>(`${this.apiUrl}/detail/${id}`);
  }

  createLocation(location: Location): Observable<Location> {
    return this.http.post<Location>(this.apiUrl, location);
  }

  updateLocation(id: string, location: Location): Observable<Location> {
    return this.http.put<Location>(`${this.apiUrl}/${id}`, location);
  }

  deleteLocation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
