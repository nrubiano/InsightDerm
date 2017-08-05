import { Component, OnInit } from '@angular/core';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-consultation-add',
  templateUrl: './consultation.add.html'
})
export class ConsultationAdd implements OnInit 
{
    /**
     * Indicate if the search box is enabled
     */
    active: boolean = true;
    /**
     * Stores the patient's indentification number 
     */
    patientId: string;
    /**
     * Ctor
     */
    constructor() { }
    /**
     * Init Event
     */
    ngOnInit() {

    }
    /**
     * Search a patient by his identification number
     */
    searchPatient() {
        alert(this.patientId);
    }
}