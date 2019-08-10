import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Patient} from "../../models/patient";
import notify from "devextreme/ui/notify";
import {Consultation} from "../../models/consultation";
import {MedicalBackground} from "../../models/medical-background";
import {PhysicalExam} from "../../models/physical-exam";
import {FileUploadControl} from "@iplab/ngx-file-upload";
import {ConsultationsService} from "../../services/consultations.services";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
    selector: 'app-consultant-details',
    templateUrl: './consultant-details.html',
    providers:[
        ConsultationsService
    ]
})
export class ConsultantDetails implements OnInit {
    @Input('isVisible') isVisible: boolean = true;
    @Input('infoTitle') infoTitle: string = 'Iniciar Consulta';
    @Input('readOnly') readOnly: boolean = false;
    @Input('patient') patient: Patient = null;

    public fileUploadControl = new FileUploadControl();
    public uploadedFiles: Array<File> = [];

    consultation: Consultation;
    medicalBackground: MedicalBackground;
    physicalExam: PhysicalExam;

    message: string;
    patientId: Patient = null;
    consultantData: Consultation = null;

    constructor(
        private router: Router,
        private activeRoute: ActivatedRoute,
        private consultationsService: ConsultationsService,
    ) { }

    ngOnInit() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();

        this.fileUploadControl.setListVisibility(false);


        this.activeRoute.params.subscribe(params => {
            if(params['id']) {
                this.consultationsService.store.byKey(params['id'])
                    .then((res: any) => {
                            this.patientId = res.patientId;
                            this.consultantData = res;
                        }, () => {
                            console.log("Some error happen loading the consultant data");
                        }
                    );
            }
        })

    }

    onCancel() {
        this.router.navigate(['/consultation']);
    }
}
