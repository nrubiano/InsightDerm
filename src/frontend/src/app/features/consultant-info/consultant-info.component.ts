import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import notify from 'devextreme/ui/notify';
import {Consultation, MedicalExam, Diagnosis, Treatment, MedicalLaboratory} from '../../models/consultation';
import {MedicalBackground} from '../../models/medical-background';
import {PhysicalExam} from '../../models/physical-exam';
import {IAlbum, Lightbox} from 'ngx-lightbox';
import {forkJoin, from} from 'rxjs';
import {ConsultationsService} from '../../services/consultations.services';
import {MedicalExamsService} from '../../services/medical-exams.services';
import {DxSelectBoxComponent} from 'devextreme-angular';
import {Router} from "@angular/router";
import {AuthenticationService} from "../../services/authentication.service";
import {flatMap, tap} from "rxjs/operators";



interface Image extends IAlbum {
    id?: any,
    fromBackend?: boolean,
    isRemoving?: boolean
}


export enum Status {
    'Open',
    'Assigned',
    'PendingDiagnosis',
    'PendingMedicalExams',
    'Closed'
}

const statusData = [
    {
        id: Status.Open,
        name: 'Abierta'
    },
    {
        id: Status.Assigned,
        name: 'Asignada'
    },
    {
        id: Status.PendingDiagnosis,
        name: 'Diagnósticos pendientes'
    },
    {
        id: Status.PendingMedicalExams,
        name: 'Exámenes médicos pendientes'
    },
    {
        id: Status.Closed,
        name: 'Cerrada'
    },
];

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
    @ViewChild('examsSelectBox') examsSelectBox: DxSelectBoxComponent;

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
    imageIndexHover: number = null;
    currentImages: Image[] = [];
    loadingImages = false;
    imageToRemove: Image = null;
    removeText: string;
    removeBasicPopup = false;
    removeBasicPopupTitle: string;
    removeBasicPopupFn: Function;
    removeBasicPopupParam: any;
    removePopup = false;
    examsPopup = false;
    examsPopover = false;
    isRemoving = false;
    columns: any[];
    currentDiagnosis: string;
    currentTreatment: string;
    currentExam: any;
    medicalLaboratory: any;
    currentExams: any;
    modalCurrentExams: any;
    modalCurrentDiagnosis: any;
    currentDiagnostics: Diagnosis[];
    currentExamsPerDiagnosis: any;
    configuredColumns: boolean = false;
    consultantStatus: any[];
    showTreatments: boolean;

    constructor(
        private consultationsService: ConsultationsService,
        private medicalExamsService: MedicalExamsService,
        private lightBox: Lightbox,
        private router: Router,
        private authService: AuthenticationService
    ) {
        this.consultantStatus = statusData;
        this.currentExams = [];
        this.currentDiagnostics = [];
        this.showTreatments = false;
    }

    ngOnInit() {
        this.medicalExamsService.store.load().then(exams => {
            this.medicalLaboratory = exams.data;
        });

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
            }
        ];

        if (this.editMode) {
            this.columns.push({
                id: 'diagnostics',
                title: 'Diagnósticos'
            })
        }

        this.configuredColumns = true;

        this.consultation = new Consultation();
        this.medicalBackground = new MedicalBackground();
        this.physicalExam = new PhysicalExam();

        if (this.consultantData) {
            this.consultation.reason = this.consultantData.reason;
            this.consultation.status = this.consultantData.status;
            this.updateExistingEntity(this.medicalBackground, this.consultantData.medicalBackground);
            this.updateExistingEntity(this.physicalExam, this.consultantData.physicalExam);
            this.currentDiagnostics = this.consultantData.diagnostics ? this.consultantData.diagnostics.map((item) => JSON.parse(item)) : [];

            this.loadingImages = true;

            this.consultationsService
                .getDiagnosticsAndExams(this.consultantData.id)
                .then((diagnostics: Diagnosis[]) => {
                    this.currentDiagnostics = diagnostics.map(diagnosis => {
                        diagnosis.exams = diagnosis.exams.map(exam => this.medicalLaboratory.find(item => item.id === exam.typeId));
                        return diagnosis
                    });
                }).catch(error => {
                console.error(error);
            });

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

    saveConsultation(alternativeMsg?: string, cb?: Function) {
        this.loading = true;
        this.consultation.medicalBackground = this.medicalBackground.json();
        this.consultation.physicalExam = this.physicalExam.json();
        this.consultation.patientId = this.patientId;

        if (this.editMode) {
            this.consultation.diagnostics = this.currentDiagnostics ? this.currentDiagnostics.map((item) => JSON.stringify(item)) : [];
        }

        const requestFn = !this.editMode ?
            this.consultationsService.insert(this.consultation) :
            this.consultationsService.update(this.consultantData, this.consultation);

        requestFn.subscribe(consultantData => {
            this.message = alternativeMsg || `La consulta fue ${!this.editMode ? 'creada' : 'actualizada'} correctamente!!`;
            notify(this.message, 'success', 2500);
            // notify('Uploading images', 'info');

            this.loading = false;

            if (!this.editMode) {
                this.restore();
            }

            if (cb) {
                cb();
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
        const observables = this.currentImages.filter(image => !image.fromBackend).map((image) => this.consultationsService.uploadImage(id, image.src));
        return forkJoin(observables);
    }

    cancel() {
        this.canceled.emit();
    }

    closeConsultantion() {
        this.consultation.status = String(Status.Closed);
        this.saveConsultation('La consulta ha sido cerrada', () => {
            this.router.navigate(['/consultation/']);
        });
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

    expandAll(e) {
        for (let i = 0; i < this.columns.length; i++) {
            e.component.expandItem(i);
        }
    }

    addDiagnosis() {
        let examPromises: any[] = [];
        const currentUser = this.authService.getCurrentUser();
        const currentDiagnosis = {
            consultationId: this.consultantData.id,
            byId: currentUser ? currentUser.id : '',
            description: this.currentDiagnosis,
            creationDate: new Date().toISOString()
        };

        this.consultationsService.postDiagnosis(this.consultantData.id, currentDiagnosis).subscribe((diagnosis: Diagnosis) => {
            this.currentExams.forEach(item => {
                examPromises.push(this.consultationsService.postMedicalLaboratories(this.consultantData.id, diagnosis.id, item))
            });

            forkJoin(examPromises).subscribe(examsResponse => {
                diagnosis.exams = this.currentExams;
                this.currentDiagnostics.push(diagnosis);

                this.currentExams = [];
                this.currentDiagnosis = '';
            });


        }, function (error) {
            console.error(error);
        });
    }
    removeTreatment(data) {
        const treatment = data[0];
        const diagnosis = data[1];

        this.isRemoving = true;
        this.removeBasicPopup = false;

        this.consultationsService.deleteTreatment(this.consultantData.id, diagnosis.id, treatment.id).subscribe(response => {
            diagnosis.treatments = diagnosis.treatments.filter((item) => item.id !== treatment.id);
            notify('El tratamiento ha sido borrado con éxito', 'success', 2500);
            this.removeBasicPopup = false;
            this.removeBasicPopupParam = null;
            this.isRemoving = false;
        },  (error) => {
            notify('Error al eliminar el tratamiento, inténtalo mas tarde', 'error', 2500);
            this.isRemoving = false;
        })
    }

    addTreatment(diagnosis: Diagnosis, treatmentDescription) {
        const currentUser = this.authService.getCurrentUser();

        if (!diagnosis.treatments) {
            diagnosis.treatments = []
        }
        const currentTreatment = {
            consultationDiagnosisId: diagnosis.id,
            description: treatmentDescription,
            byId: currentUser ? currentUser.id : '',
            creationDate: new Date().toISOString()
        };

        this.consultationsService.postTreatment(this.consultantData.id, diagnosis.id, currentTreatment).subscribe(response => {
            diagnosis.treatments.push(response);
            diagnosis.currentTreatment = '';
        });


    }

    addExam(event) {
        if (this .examsSelectBox.value) {
            const examExists = this.currentExams.find((exam) => exam.id === this.examsSelectBox.value);

            if (!examExists) {
                this.currentExams.push({
                    id: this.examsSelectBox.value,
                    name: this.examsSelectBox.text,
                });
            }

            this.examsSelectBox.instance.reset();
        }
    }

    removeExam(examRemove: any) {
        this.currentExams = this.currentExams.filter((exam) => exam.id !== examRemove.id)
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

    removeDiagnosis(diagnosisItem: Diagnosis) {
        this.isRemoving = true;
        this.removeBasicPopup = false;

        this.consultationsService.deleteDiagnosis(this.consultantData.id, diagnosisItem.id).subscribe(() => {
            this.currentDiagnostics = this.currentDiagnostics.filter((item) => item.id !== diagnosisItem.id);
            notify('El diagnóstico ha sido borrado con éxito', 'success', 2500);
            this.removeBasicPopup = false;
            this.removeBasicPopupParam = null;
            this.isRemoving = false;
        },  (error) => {
            notify('Error al eliminar el diagnóstico, inténtalo mas tarde', 'error', 2500);
            this.isRemoving = false;
        })

        //this.currentDiagnostics.splice(DiagnosisIndex, 1);
    }

    viewExams(exams) {
        this.modalCurrentExams = exams;
        this.examsPopup = true;
    }

    openRemoveTreatmentsPopup(treatment: Treatment, diagnosis: Diagnosis) {
        this.removeBasicPopupParam = [treatment, diagnosis];
        this.removeBasicPopupTitle = '¿Deseas eliminar el tratamiento?';
        this.removeBasicPopupFn = this.removeTreatment;
        this.removeBasicPopup = true;
    }

    openRemoveDiagnosisPopup(diagnosisItem: Diagnosis) {
        this.removeBasicPopupParam = diagnosisItem;
        this.removeBasicPopupTitle = '¿Deseas eliminar el diagnóstico?';
        this.removeBasicPopupFn = this.removeDiagnosis;
        this.removeBasicPopup = true;
    }

    openRemovePopup(image: Image) {
        this.imageToRemove = image;
        this.removePopup = true;
    }
}
