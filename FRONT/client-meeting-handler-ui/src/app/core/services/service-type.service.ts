import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ServiceType, ServiceTypeDetail} from '../models/service-type.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ServiceTypeService {
  private apiUrl = `${environment.apiUrl}/serviceType`;

  constructor(private http: HttpClient) { }

  getAllServiceTypes(): Observable<ServiceType[]> {
    return this.http.get<ServiceType[]>(`${this.apiUrl}/simple`);
  }

  getServiceTypeById(id: string): Observable<ServiceType> {
    return this.http.get<ServiceType>(`${this.apiUrl}/simple/${id}`);
  }

  getAllServiceTypeDetails(): Observable<ServiceTypeDetail[]> {
    return this.http.get<ServiceTypeDetail[]>(`${this.apiUrl}/detail`);
  }

  getServiceTypeDetail(id: string): Observable<ServiceTypeDetail> {
    return this.http.get<ServiceTypeDetail>(`${this.apiUrl}/detail/${id}`);
  }

  createServiceType(serviceType: ServiceType): Observable<ServiceType> {
    return this.http.post<ServiceType>(this.apiUrl, serviceType);
  }

  updateServiceType(id: string, serviceType: ServiceType): Observable<ServiceType> {
    return this.http.put<ServiceType>(`${this.apiUrl}/${id}`, serviceType);
  }

  deleteServiceType(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
