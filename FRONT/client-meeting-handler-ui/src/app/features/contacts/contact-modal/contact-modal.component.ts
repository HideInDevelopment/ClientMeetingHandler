import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Contact } from '../../../core/models/contact.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact-modal.component.html'
})
export class ContactModalComponent implements OnInit, OnChanges {
  @Input() isOpen = false;
  @Input() email = '';
  @Input() contactData: Contact | null = null;
  @Input() isEditMode = false;

  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<Contact>();

  contactForm: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder) {
    this.contactForm = this.fb.group({
      Id: [this.generateGuid()],
      Email: ['', [Validators.required, Validators.email]],
      Country: ['', Validators.required],
      PhoneNumber: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      ClientId: ['']
    });
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    // If contactData changes, reinitialize the form
    if (changes['contactData'] || changes['isOpen'] || changes['isEditMode']) {
      console.log("Modal isEditMode changed to:", this.isEditMode);
      console.log("Modal contactData:", this.contactData);
      this.initializeForm();
    }
  }

  initializeForm(): void {
    // Reset form
    this.contactForm.reset();

    console.log("Initializing form. isEditMode:", this.isEditMode, "contactData:", this.contactData);

    if (this.isEditMode && this.contactData) {
      // In edit mode, populate form with existing contact data
      this.contactForm.patchValue({
        Id: this.contactData.Id,
        Email: this.contactData.Email,
        Country: this.contactData.Country,
        PhoneNumber: this.contactData.PhoneNumber.toString(),
        ClientId: this.contactData.ClientId
      });
      console.log("Form initialized in EDIT mode with data:", this.contactForm.value);
    } else {
      // In add mode, initialize with new ID and email if provided
      this.contactForm.patchValue({
        Id: this.generateGuid(),
        Email: this.email || '',
        ClientId: ''
      });
      console.log("Form initialized in CREATE mode with data:", this.contactForm.value);
    }
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.contactForm.invalid) {
      return;
    }

    const contactData: Contact = {
      Id: this.contactForm.value.Id,
      Email: this.contactForm.value.Email,
      Country: this.contactForm.value.Country,
      PhoneNumber: Number(this.contactForm.value.PhoneNumber),
      ClientId: this.contactForm.value.ClientId
    };

    this.save.emit(contactData);
  }

  onCancel(): void {
    this.close.emit();
  }

  private generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      const r = Math.random() * 16 | 0,
        v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}
