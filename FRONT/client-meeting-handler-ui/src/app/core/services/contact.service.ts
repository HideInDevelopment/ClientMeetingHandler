import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {Contact, ContactDetail} from '../models/contact.model';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private apiUrl = `${environment.apiUrl}/contact`;

  constructor(private http: HttpClient) { }

  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.apiUrl}/simple`);
  }

  getAvailableContacts(): Observable<Contact[]>{
    return this.http.get<Contact[]>(`${this.apiUrl}/simple`)
    .pipe(
      map(contacts => contacts.filter(contact => !contact.ClientId))
    )
  }

  getContactById(id: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/simple/${id}`);
  }

  getContactByEmail(email: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/simple/${email}`);
  }

  getAllContactDetails(): Observable<ContactDetail[]> {
    return this.http.get<ContactDetail[]>(`${this.apiUrl}/detail`);
  }

  getContactDetail(id: string): Observable<ContactDetail> {
    return this.http.get<ContactDetail>(`${this.apiUrl}/detail/${id}`);
  }

  getContactDetailByEmail(email: string): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/detail/${email}`);
  }

  createContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact);
  }

  updateContact(id: string, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${this.apiUrl}/${id}`, contact);
  }

  deleteContact(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
