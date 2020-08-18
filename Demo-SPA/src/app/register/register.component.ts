import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Role } from '../_models/role';
import { FormControl, FormGroup, FormArray } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  roles: Role[];
  model: any = {};

  registerForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    role: new FormArray([]),
  });

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.loadPerm();
  }

  loadPerm() {
    this.authService.getRoles().subscribe((roles: Role[]) => {
      this.roles = roles;
    }, error => {
      console.log('Error');
    });
  }

  onCheckChange(event){
    let index: number = -1;

    if (event.target.checked){
      this.role.push(new FormControl(event.target.value));
      index = index + 1;
    }else {
      this.role.removeAt(index);
      index -= 1;
    }
    console.log(this.registerForm.value);
  }

  get role() {
    return this.registerForm.get('role') as FormArray;
  }


  register() {
    this.authService.register(this.registerForm.value).subscribe(() => {
      console.log('Registration successful');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
