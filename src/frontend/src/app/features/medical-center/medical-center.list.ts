import { Component, OnInit } from '@angular/core';
import { DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-medical-center',
  templateUrl: './medical-center.list.html'
})

export class MedicalCenterList implements OnInit {
  
    medicalCenters: any[];

    displayDialog: boolean;

    medicalCenter: any = {};
    
    selectedMedicalCenter: any;
    
    newMedicalCenter: boolean;

    ngOnInit() {
        this.medicalCenters = [
            {
                Name: "Medical Center 1",
                City: "Bogota"
            },
            {
                Name: "Medical Center 2",
                City: "Bogota"
            },
            {
                Name: "Medical Center 2",
                City: "Medellin"
            }
        ];
    }

    showDialogToAdd() {
        this.newMedicalCenter = true;
        this.medicalCenter = { Name: "", City: "" };
        this.displayDialog = true;
    }
}