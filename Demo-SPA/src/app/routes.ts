import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ValueComponent } from './value/value.component';
import { UsersComponent } from './users/users.component';
import { AuthGuard } from './_guards/auth.guard';



export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    { 
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'values', component: ValueComponent},
            { path: 'users', component: UsersComponent},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
];
