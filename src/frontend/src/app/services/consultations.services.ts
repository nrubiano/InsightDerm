import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import {flatMap, map, tap} from 'rxjs/operators';
import {HttpClient, HttpParams} from '@angular/common/http';
import {forkJoin, from, Observable, pipe} from 'rxjs';
import {Consultation, Diagnosis} from 'app/models/consultation';

/**
 * Consultations Service
 */
@Injectable()
export class ConsultationsService {
    store: CustomStore;
    private api: string;

    constructor(private http: HttpClient) {
        this.setupStore();
        this.api = AppSettings.API + '/consultations';
    }

    /**
     * Setup the store with the http methods
     */
    setupStore() {
        const http = this.http;
        this.store = new CustomStore({
            byKey: (key): Promise<any> => {
                return http.get(`${this.api}/${key}`)
                    .toPromise()
                    .then(response => response)
                    .catch(error => { throw new Error('Data Loading Error') });
            },
            insert: (item): Promise<any> => {
                return  this.insert(item)
                    .toPromise();
            },
            load: (loadOptions): Promise<any> => {
                let params: HttpParams = new HttpParams();
                [
                    'skip',
                    'take',
                    'requireTotalCount',
                    'requireGroupCount',
                    'sort',
                    'filter',
                    'totalSummary',
                    'group',
                    'groupSummary'
                ].forEach(function(i) {
                    if (i in loadOptions && isNotEmpty(loadOptions[i])) {
                        params = params.set(i, JSON.stringify(loadOptions[i]));
                    }
                });
                return this.http.get<any[]>(this.api, { params: params })
                    .toPromise()
                    .then(result => {
                        return {
                            data: result,
                            totalCount: result.length
                        };
                    });
            },
            update: (entity, updatedValues): Promise<any> => {
                return this.update(entity, updatedValues)
                    .toPromise()
                    .then(response => {
                        const json = response.json();
                        return {
                            data: json
                        }
                    })
                    .catch(error => { throw new Error('Data Update Error') });
            },
            remove: (key): Promise<any> => {
                return http.delete(this.api + '/' + encodeURIComponent(key.id))
                    .toPromise()
                    .then(response => {
                        const json = response;
                        return {
                            data: json
                        }
                    })
                    .catch(error => {
                        console.log(error);
                        throw new Error('Data Update Error')
                    });
            }
        });
    }

    getAll(): Promise<Consultation[]>{
        return this.http.get<Consultation[]>(this.api)
            .toPromise()
            .then(result => {
                return result
            });
    }

    test(): Promise<Consultation[]>{
        return this.http.get<any>(AppSettings.API + '/medicallaboratorytypes')
            .toPromise()
            .then(result => {
                return result
            });
    }

    insert(consultation: Consultation): Observable<Consultation> {
        return  this.http
            .post<Consultation>(this.api, consultation);
    }

    update(consultation: Consultation, updated: Consultation): Observable<Consultation> {
        return this.http
            .put<Consultation>(this.api + '/' + encodeURIComponent(consultation.id), {...consultation, ...updated});
    }

    removeImage(consultationId, imageId) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/images/${imageId}`;
        return this.http.delete(api);
    }

    uploadImage(consultationId, image) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/images`;
        const http = this.http;
        const payload = {
            consultationId,
            image
        };

        return http.post(api, payload);
    }

    getImages(consultationId): Observable<any> {
        const api =  `${AppSettings.API}/consultations/${consultationId}/images`;
        return this.http.get(api);
    }

    postDiagnosis(consultationId, diagnosis: Diagnosis) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis`;
        return this.http.post(api, diagnosis);
    }

    deleteDiagnosis(consultationId, diagnosisId) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}`;
        return this.http.delete(api);
    }

    getDiagnosticsAndExams(consultationId) {
        return new Promise((resolve, reject) => {
            const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis`;
            this.http.get(api).subscribe((diagnosticsList: Diagnosis[]) => {
                let promisesArray = [];
                diagnosticsList.forEach(item => {
                    promisesArray.push(
                        this.getMedicalLaboratoies(consultationId, item.id).pipe(
                            map((exams: any) => {
                                item.exams = exams;
                                return item
                            })
                        )
                    );
                });


                forkJoin(promisesArray).subscribe(data => {
                    promisesArray = [];
                    data.forEach(diagnosticItem => {
                        promisesArray.push(
                            this.getTreatments(consultationId, diagnosticItem.id).pipe(
                                map((treatments: any) => {
                                    diagnosticItem.treatments = treatments;
                                    return diagnosticItem
                                })
                            )
                        );
                    });

                    forkJoin(promisesArray).subscribe(data => resolve(data));
                });
            });
        })
    }

    postMedicalLaboratories(consultationId, diagnosisId, laboratoryInstance) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}/medical-laboratories`;
        return this.http.post(api, {
            diagnosisId,
            typeId: laboratoryInstance.id
        });
    }

    getMedicalLaboratoies(consultationId, diagnosisId) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}/medical-laboratories`;
        return this.http.get(api);
    }

    postTreatment(consultationId, diagnosisId, treatment) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}/treatments`;
        return this.http.post(api, treatment);
    }

    getTreatments(consultationId, diagnosisId) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}/treatments`;
        return this.http.get(api);
    }


    deleteTreatment(consultationId, diagnosisId, treatmentId) {
        const api =  `${AppSettings.API}/consultations/${consultationId}/diagnosis/${diagnosisId}/treatments/${treatmentId}`;
        return this.http.delete(api);
    }

}

function isNotEmpty(value: any): boolean {
    return value !== undefined && value !== null && value !== '';
}
