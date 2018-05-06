import { Component } from '@angular/core';
import {Observable} from 'rxjs'
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';;




const states = ['Alabama', 'Alaska', 'American Samoa', 'Arizona', 'Arkansas', 'California', 'Colorado',
  'Connecticut', 'Delaware', 'District Of Columbia', 'Federated States Of Micronesia', 'Florida', 'Georgia',
  'Guam', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine',
  'Marshall Islands', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana',
  'Nebraska', 'Nevada', 'New Hampshire', 'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
  'Northern Mariana Islands', 'Ohio', 'Oklahoma', 'Oregon', 'Palau', 'Pennsylvania', 'Puerto Rico', 'Rhode Island',
  'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont', 'Virgin Islands', 'Virginia',
  'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'];

@Component({
  selector: 'cat-page',
  templateUrl: './ng-bootstrap.html'
})

export class ComponentsNgBootstrap {

  // Modal Open
  constructor(private modalService: NgbModal) {}
  openModal(content) {
    this.modalService.open(content);
  }

  // Pagination Example
  paginationCurrent = 3;

  // Rating Example
  ratingCurrent = 8;

  // Timepicker Example
  timepickerModel = {hour: 13, minute: 30};
}
