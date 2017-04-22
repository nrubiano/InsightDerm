import { Component, OnInit } from '@angular/core';
import { DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-doctor-list',
  templateUrl: './doctor.list.html'
})

export class DoctorList implements OnInit {
  
    doctors: any[];

    displayDialog: boolean;

    doctor: any = {};
    
    selectedDoctor: any;
    
    new: boolean;

    ngOnInit() {
        this.doctors = [
            {
                Name: "Nicolas Rubiano",
                Identification: "1019038492",
                Phone: "7971010",
                Cellphone: "3007971010",
                Email: "nicolas.rubiano@colmedica.com",
                City: "Bogota"
            }
        ];
    }

    showDialogToAdd() {
        this.new = true;
        this.doctor = {};
        this.displayDialog = true;
    }
}