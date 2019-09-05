import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    LoginComponent
  ]
})

export class LoginModule { }
