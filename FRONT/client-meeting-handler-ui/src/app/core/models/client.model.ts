import {Contact} from "./contact.model";
import {Meeting} from "./meeting.model";
import {Service} from "./service.model";

export interface Client {
    id: string;
    name: string;
    contactId: string;
}

export interface ClientDetail extends Client {
    contact: Contact;
    meetings: Meeting[];
    services: Service[];
}
