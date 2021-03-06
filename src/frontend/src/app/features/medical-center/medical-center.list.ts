import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { MedicalCentersService } from '../../services/medical-centers.services';
import { CitiesService } from '../../services/cities.services';

declare var $: any;
declare var jQuery: any;

@Component({
  selector: 'fea-medical-center',
  templateUrl: './medical-center.list.html',
  providers:[
    MedicalCentersService,
    CitiesService
  ]
})

export class MedicalCenterList implements OnInit {
  
    medicalCenters: CustomStore;

    cities: CustomStore

    constructor(private medicalCenterService : MedicalCentersService, private citiesService : CitiesService) {}

    ngOnInit() {
        this.medicalCenters = this.medicalCenterService.store;
        this.cities = this.citiesService.store;
    }
}