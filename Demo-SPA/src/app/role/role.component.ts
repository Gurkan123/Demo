import { Component, OnInit } from '@angular/core';
import { Perm } from '../_models/perm';
import { RoleService } from '../_services/role.service';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})



export class RoleComponent implements OnInit {
  perms: Perm[];
  model: any = {};


  permissionForm = new FormGroup({
    roleName: new FormControl(''),
    permissions: new FormArray([]),
  });

  constructor(private roleService: RoleService) { }

  ngOnInit() {
    this.loadPerm();
  }

  loadPerm() {
    this.roleService.getPerms().subscribe((perms: Perm[]) => {
      this.perms = perms;
    }, error => {
      console.log('Error');
    });
  }

  onCheckChange(event){
    let index: number = -1;

    if (event.target.checked){
      this.permissions.push(new FormControl(event.target.value));
      index = index + 1;
    }else {
      this.permissions.removeAt(index);
      index -= 1;
    }
    console.log(this.permissionForm.value);
  }

  get permissions() {
    return this.permissionForm.get('permissions') as FormArray;
  }

  postRole() {
    console.log(this.permissionForm.value);
    this.roleService.postRole(this.permissionForm.value).subscribe((data) => {
    console.log(data);
    this.postRolePerm();
     }, error => {
       console.log('Error post');
    });
  }

  postRolePerm() {
    this.roleService.postRolePerm(this.permissionForm.value).subscribe((data) => {
      console.log(data);
    }, error => {
      console.log('Error PostRolePerm');
    });
  }

}
