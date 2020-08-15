import { Component, OnInit } from '@angular/core';
import { Perm } from '../_models/perm';
import { RoleService } from '../_services/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  perms: Perm[];
  model: any = {};


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

  postRole() {
    this.roleService.postRole(this.model).subscribe((data) => {
      console.log(data);
    }, error => {
      console.log('Error post');
    });
  }

}
