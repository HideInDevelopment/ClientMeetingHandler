import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Contact} from '../../../core/models/contact.model';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-contact-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './contact-modal.component.html'
})
export class ContactModalComponent implements OnInit {
  @Input() isOpen = false;
  @Input() email = '';
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
    if (this.email) {
      this.contactForm.patchValue({ Email: this.email });
    }
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.contactForm.invalid) {
      return;
    }

    const newContact: Contact = {
      Id: this.contactForm.value.Id,
      Email: this.contactForm.value.Email,
      Country: this.contactForm.value.Country,
      PhoneNumber: Number(this.contactForm.value.PhoneNumber),
      ClientId: this.contactForm.value.ClientId
    };

    this.save.emit(newContact);
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
