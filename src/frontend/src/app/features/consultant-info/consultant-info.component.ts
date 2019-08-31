import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import notify from 'devextreme/ui/notify';
import {Consultation, MedicalExam, Treatment} from '../../models/consultation';
import {MedicalBackground} from '../../models/medical-background';
import {PhysicalExam} from '../../models/physical-exam';
import {IAlbum, Lightbox} from 'ngx-lightbox';
import {forkJoin} from 'rxjs';
import {ConsultationsService} from '../../services/consultations.services';
import {MedicalExamsService} from '../../services/medical-exams.services';
import {DxSelectBoxComponent} from 'devextreme-angular';



interface Image extends IAlbum {
    id?: any,
    fromBackend?: boolean,
    isRemoving?: boolean
}

@Component({
    selector: 'app-consultant-info',
    templateUrl: './consultant-info.component.html',
    styleUrls: ['./consultant-info.component.css'],
    providers: [
        ConsultationsService,
        MedicalExamsService
    ]
})

export class ConsultantInfoComponent implements OnInit {
    @ViewChild(DxSelectBoxComponent) examsSelectBox: DxSelectBoxComponent

    @Input('isVisible') isVisible = true;
    @Input('infoTitle') infoTitle = 'Iniciar Consulta';
    @Input('saveText') saveText = 'Guardar Interconsulta';
    @Input('patientId') patientId: any = null;
    @Input('consultantData') consultantData: Consultation = null;
    @Input('editMode') editMode = false;
    @Output() canceled = new EventEmitter();
    @Output() restored = new EventEmitter();

    consultation: Consultation;
    medicalBackground: MedicalBackground;

    physicalExam: PhysicalExam;

    message: string;
    loading = false;

    value: any[] = [];
    imagesUlr: string;
    imageIndexHover: number = null;
    currentImages: Image[] = [];
    loadingImages = false;
    imageToRemove: Image = null;
    removePopup = false;
    isRemoving = false;
    columns: any[];
    currentTreatment: string;
    currentExam: any;
    medicalExams: any[];
    currentExams: MedicalExam[];
    currentTreatments: Treatment[];

    constructor(
        private consultationsService: ConsultationsService,
        private medicalExamsService: MedicalExamsService,
        private lightBox: Lightbox
    ) {
        this.columns = [
            {
                id: 'consultationReason',
                title: 'Motivo Consulta'
            }, {
                id: 'record',
                title: 'Antecedentes'
            }, {
                id: 'physicalExam',
                title: 'Examen F&iacute;sico '
            }, {
                id: 'diagnosticImages',
                title: 'Im&aacute;genes Diagnosticas'
            },
        ];

        if (this.editMode) {
            this.columns.push({
                id: 'treatment',
                title: 'Tratamiento'
            });
        }

        /*this.medicalExamsService.store.load().then((exams) => {
            console.warn(exams);
            this.medicalExams = exams.data;
        }).catch((error) => {
            console.error('Error', error);
        });*/

        this.medicalExams = [
            {
                id: 0,
                name: 'Examen de sangre'
            },
            {
                id: 1,
                name: 'Examen de orina'
            },
            {
                id: 2,
                name: 'Examen auditivo'
            },
            {
                id: 3,
                name: 'Resonancia magnetica'
            },
            {
                id: 4,
                name: 'Radriografia'
            },
            {
                id: 5,
                name: 'Examen de glucosa'
            }
        ];
        this.currentExams = [];
    }

    ngOnInit() {
        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();

        if (this.consultantData) {
            this.consultation.reason = this.consultantData.reason;
            this.updateExistingEntity(this.medicalBackground, this.consultantData.medicalBackground);
            this.updateExistingEntity(this.physicalExam, this.consultantData.physicalExam);
            this.currentTreatments = this.consultantData.treatments ? this.consultantData.treatments.map((item) => JSON.parse(item)) : [];

            this.imagesUlr = `localhost:5000/api/v1/consultations/${this.consultantData.id}/images`;

            this.loadingImages = true;

            this.consultationsService.getImages(this.consultantData.id).subscribe((images) => {
                images.map((img) => {
                    this.currentImages.push({
                        id: img.id,
                        src: img.image,
                        thumb: img.image,
                        fromBackend: true
                    });
                });

                this.loadingImages = false;
            }, (error) => {
                notify('Error obteniendo las imagenes', 'error', 2500);
            })
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

        if (this.editMode) {
            this.consultation.treatments = this.currentTreatments.map((item) => JSON.stringify(item));
        }

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
                notify('Las imagenes han sido agregadas', 'success', 2500);

            }, error => {
                notify('Error agregando las imagenes', 'error', 2500);
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
        this.currentImages.forEach((image) => {
            if (!image.fromBackend) {
                observables.push(this.consultationsService.uploadImage(id, image.src))
            }
        });
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
            this.message = 'Solo se soportan imagenes.';
            return;
        }

        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (_event) => {
            this.currentImages.push({
                id: Math.random().toString(36).substring(7),
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

    openRemovePopup(image: Image) {
        this.imageToRemove = image;
        this.removePopup = true;
    }

    removeImage(image: Image) {
        if (image.fromBackend) {
            this.isRemoving = true;

            this.consultationsService.removeImage(this.consultantData.id, image.id).subscribe(() => {
                this.currentImages = this.currentImages.filter((item) => item.id !== image.id);
                notify('La imagen ha sido borrada con éxito', 'success', 2500);
                this.removePopup = false;
                this.imageToRemove = null;
                this.isRemoving = false;
            },  (error) => {
                notify('Error al eliminar la imagen, inténtalo mas tarde', 'error', 2500);
                this.isRemoving = false;
            })
        } else {
            this.currentImages = this.currentImages.filter((item) => item.id !== image.id);
            this.removePopup = false;
            this.imageToRemove = null;
            notify('La imagen ha sido borrada con éxito', 'success', 2500);
        }
    }

    expandAll(e) {
        for (let i = 0; i < this.columns.length; i++) {
            e.component.expandItem(i);
        }
    }

    addTreatment() {
        this.currentTreatments.push({
            date: Date.now(),
            description: this.currentTreatment,
            by: 'Current User',
            medicalExams: this.currentExams
        });

        this.currentExams = [];

        this.currentTreatment = '';
        this.examsSelectBox.instance.reset();
    }

    addExam(event) {
        if (this.currentExam) {
            const examExists = this.currentExams.find((exam) => exam.medicalExamId === this.currentExam.id);

            if (!examExists) {
                this.currentExams.push({
                    medicalExamId: this.currentExam.id,
                    result: {
                        date: Date.now(),
                        description: this.currentExam.name
                    }
                });
            }

            this.examsSelectBox.instance.reset();
        }
    }

    removeExam(examRemove: MedicalExam) {
        this.currentExams = this.currentExams.filter((exam) => exam.medicalExamId !== examRemove.medicalExamId)
    }

    removeTreatment(treatmentIndex: any) {
        this.currentTreatments.splice(treatmentIndex, 1);
    }
}
