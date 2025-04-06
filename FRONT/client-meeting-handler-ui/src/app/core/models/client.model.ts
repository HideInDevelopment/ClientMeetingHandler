import {Contact} from "./contact.model";
import {Meeting} from "./meeting.model";
import {Service} from "./service.model";

export interface Client {
    Id: string;
    Name: string;
    ContactId: string;
}

export interface ClientDetail extends Client {
    Contact: Contact;
    Meetings: Meeting[];
    Services: Service[];
}
