import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';
import { Routes, RouterModule }  from '@angular/router';

import {
    DxDateBoxModule,
    DxTextAreaModule,
    DxNumberBoxModule,
    DxPopupModule,
    DxDataGridModule,
    DxButtonModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxFileUploaderModule,
    DxAccordionModule,
    DxDropDownBoxModule,
    DxLookupModule, DxSelectBoxModule
} from 'devextreme-angular'

import { ConsultationList } from './consultation.list'
import { ConsultationAdd } from './consultation.add'
import { PatientFormComponent } from '../../components/patient-form/patient-form.component';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ConsultantInfoComponent} from "../consultant-info/consultant-info.component";
import {ConsultantDetails} from "./consultant-details";
import {LightboxModule} from "ngx-lightbox";
import {NgxImageZoomModule} from "ngx-image-zoom";
import {NgxMasonryModule} from "ngx-masonry";

export const routes: Routes = [
    { path: 'consultation', component: ConsultationList },
    { path: 'consultation/add', component: ConsultationAdd },
    { path: 'consultation/:id', component: ConsultantDetails }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        DxDateBoxModule,
        DxTextAreaModule,
        DxTextBoxModule,
        DxValidatorModule,
        DxNumberBoxModule,
        DxPopupModule,
        DxDataGridModule,
        DxButtonModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        RouterModule.forChild(routes),
        DxFileUploaderModule,
        LightboxModule,
        NgxImageZoomModule,
        NgxMasonryModule,
        DxAccordionModule,
        DxDropDownBoxModule,
        DxLookupModule,
        DxSelectBoxModule
    ],
    declarations: [
        ConsultationList,
        ConsultationAdd,
        ConsultantDetails,
        PatientFormComponent,
        ConsultantInfoComponent
    ]

})

export class ConsultationModule { }
