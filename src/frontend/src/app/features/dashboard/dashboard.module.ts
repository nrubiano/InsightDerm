import { FormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';
import {DashboardComponent} from "./dashboard.component";
import {AuthGuard} from "../../infrastructure/guards/auth.guard";

export const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent,  canActivate: [AuthGuard]}
];

@NgModule({
  imports: [    
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],

  declarations: [
    DashboardComponent
  ]
})

export class DashboardModule { }
