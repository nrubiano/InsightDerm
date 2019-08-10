import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import notify from 'devextreme/ui/notify';
import {Consultation} from '../../models/consultation';
import {MedicalBackground} from '../../models/medical-background';
import {PhysicalExam} from '../../models/physical-exam';
import {FileUploadControl} from '@iplab/ngx-file-upload';
import {ConsultationsService} from '../../services/consultations.services';
import {Http} from '@angular/http';
import {Lightbox} from 'ngx-lightbox';
import {forkJoin} from 'rxjs';


interface Image {
    src: string,
    caption?: string;
    thumb: string;
}

@Component({
    selector: 'app-consultant-info',
    templateUrl: './consultant-info.component.html',
    styleUrls: ['./consultant-info.component.css'],
    providers: [
        ConsultationsService
    ]
})

export class ConsultantInfoComponent implements OnInit {
    @Input('isVisible') isVisible = true;
    @Input('infoTitle') infoTitle = 'Iniciar Consulta';
    @Input('saveText') saveText = 'Guardar Interconsulta';
    @Input('patientId') patientId: any = null;
    @Input('consultantData') consultantData: Consultation = null;
    @Input('editMode') editMode = false;
    @Output() canceled = new EventEmitter();
    @Output() restored = new EventEmitter();

    public fileUploadControl = new FileUploadControl();
    public uploadedFiles: Array<File> = [];

    consultation: Consultation;
    medicalBackground: MedicalBackground;

    physicalExam: PhysicalExam;

    message: string;
    loading = false;

    value: any[] = [];
    imagesUlr: string;
    currentImages: Image[] = [];

    constructor(
        private consultationsService: ConsultationsService,
        private lightBox: Lightbox
    ) { }

    ngOnInit() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();

        this.fileUploadControl.setListVisibility(false);

        if (this.consultantData) {
            this.consultation.reason = this.consultantData.reason;
            this.updateExistingEntity(this.medicalBackground, this.consultantData.medicalBackground);
            this.updateExistingEntity(this.physicalExam, this.consultantData.physicalExam);

            this.imagesUlr = `localhost:5000/api/v1/consultations/${this.consultantData.id}/images`;
        }
    }

    updateExistingEntity(entity, existingData) {
        const parsedData = JSON.parse(existingData);
        Object.keys(parsedData).forEach((key) => entity[key] = parsedData[key]);
    }

    saveConsultation() {
        this.loading = true;
        this.consultation.medicalBackground = this.medicalBackground.json();
        this.consultation.physicalExam = this.physicalExam.json();
        this.consultation.patientId = this.patientId;

        const requestFn = !this.editMode ?
            this.consultationsService.insert(this.consultation) :
            this.consultationsService.update(this.consultantData, this.consultation);

        requestFn.subscribe(consultantData => {
            this.message = `La consulta fue ${!this.editMode ? 'creada' : 'actualizada'} correctamente!!`;
            notify(this.message, 'success', 2500);
            // notify('Uploading images', 'info');

            this.loading = false;

            if (!this.editMode) {
                this.restore();
            }

            this.uploadImages(consultantData.id).subscribe(value => {
                this.loading = false;
                if (!this.editMode) {
                    this.restore();
                }
                notify('Images have been uploaded', 'success', 2500);

            }, error => {
                notify('Error papu', 'error', 2500);
                this.loading = false;
                if (!this.editMode) {
                    this.restore();
                }
            })


        }, error => {
            this.message = `No se logro procesar la consulta, por favor intente de nuevo más tarde.`;
            notify(this.message, 'error', 2500);
            this.loading = false;
        });



    }

    uploadImages(id) {
        const observables = [];
        this.currentImages.forEach((image) => observables.push(this.consultationsService.uploadImage(id, image.src)));
        return forkJoin(observables);
    }

    cancel() {
        this.canceled.emit();
    }

    restore() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();
        this.isVisible = false;
        this.restored.emit();
    }

    onFileAdded(event) {
        this.value.map((image) => this.createPreview(image))
    }

    createPreview(file: any) {
        const mimeType = file.type;

        if (mimeType.match(/image\/*/) == null) {
            this.message = 'Only images are supported.';
            return;
        }

        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (_event) => {
            this.currentImages.push({
                src: reader.result,
                thumb: reader.result,
                caption: file.name,
            })
        }
    }

    viewImage(index) {
        // open lightbox
        if (this.editMode) {
            this.lightBox.open(this.currentImages, index, {
                centerVertically: true
            });
        }
    }

    close(): void {
        // close lightbox programmatically
        this.lightBox.close();
    }
}
