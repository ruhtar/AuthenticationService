import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserAggregate } from 'src/app/interfaces/IUserAggregate';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent {
  @Input() formTitle!: string;

  userForm!: FormGroup;
  formSucess: boolean = false;
  @Output() onSubmit = new EventEmitter<IUserAggregate>(); //Sa�da. Enviar dados para o componente pai

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.userForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      isSeller: [''],
      street: ['', Validators.required],
      streetNumber: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
    });
  }

  submitForm() {
    //if (this.userForm.invalid) return;
    alert('submit');
    this.onSubmit.emit(this.userForm.value);
    this.userForm.reset();
    this.formSucess = true;
  }
}
