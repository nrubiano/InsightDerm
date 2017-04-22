import { Component, OnInit } from '@angular/core';
import { DataTableModule,SharedModule,DialogModule,ButtonModule} from 'primeng/primeng';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-consultation-list',
  templateUrl: './consultation.list.html'
})

export class ConsultationList implements OnInit {
  
    consultations: any[];

    displayDialog: boolean;

    consultation: any = {};
    
    selectedConsultation: any;
    
    new: boolean;

    ngOnInit() {
        this.consultations = [
            {
                Name: "Camilo Sandoval",
                Identification: "1019012392",
                Date: "2017-01-01",                
                City: "Bogota",
                Diagnostic: "Picadura de Mosquito",
                Question: "Es posible que existan complicaciones con el tratamiento, acoseja aplicar penecilina?"
            }
        ];
    }

    showDialogToAdd() {
        this.new = true;
        this.displayDialog = true;
    }
}