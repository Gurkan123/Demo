import { Component, OnInit, ViewChild } from '@angular/core';
import { Value } from '../_models/value';
import { ValueService } from '../_services/value.service';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: Value[];
  model: any = {};

  constructor(private valueService: ValueService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.loadValue();
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

  postValue() {
    this.valueService.postValue(this.model).subscribe((data) => {
      console.log(data);
      this.loadValue();
    }, error => {
      console.log('Error post');
    });
  }

}
