import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule }      from '@angular/core';
import { CommonModule }  from '@angular/common';

import {  DxDateBoxModule,
    DxTextAreaModule,
    DxNumberBoxModule,
    DxPopupModule,
    DxButtonModule } from 'devextreme-angular'

import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        DxDateBoxModule,
        DxTextAreaModule,
        DxNumberBoxModule,
        DxPopupModule,
        DxButtonModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
    ],
    declarations: []

})

export class ConsultationInfoModule { }
