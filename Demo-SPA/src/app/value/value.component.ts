import { Component, OnInit } from '@angular/core';
import { Value } from '../_models/value';
import { ValueService } from '../_services/value.service';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: Value[];
  role: any;

  constructor(private valueService: ValueService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.loadValue();
    const role = localStorage.getItem('role');
    console.log(role);
  }

  loadValue() {
    this.valueService.getValues().subscribe((values: Value[]) => {
      this.values = values;
    }, error => {
      console.log('Error');
    });
  }

  deleteValue(id: number) {
    this.valueService.deleteValue(id).subscribe(() => {
      this.loadValue();
    });
  }

}
