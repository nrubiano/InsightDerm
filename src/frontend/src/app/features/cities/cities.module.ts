import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import { CitiesList } from './cities.list'

export const routes: Routes = [
  { path: 'cities/list', component: CitiesList }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    CitiesList
  ]

})

export class CitiesModule { }
