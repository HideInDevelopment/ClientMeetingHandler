import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Contact, ContactDetail} from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  // TODO: Modify URL
  private apiUrl = "http://localhost:5162/contact";

  constructor(private http: HttpClient) { }

  getAllContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.apiUrl}/simple`);
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
