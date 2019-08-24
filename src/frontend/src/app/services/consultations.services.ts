import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import { map } from 'rxjs/operators';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Consultation } from 'app/models/consultation';

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
                    "skip",
                    "take",
                    "requireTotalCount",
                    "requireGroupCount",
                    "sort",
                    "filter",
                    "totalSummary",
                    "group",
                    "groupSummary"
                ].forEach(function(i) {
                    if(i in loadOptions && isNotEmpty(loadOptions[i]))
                        params = params.set(i, JSON.stringify(loadOptions[i]));
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
                                    let json = response.json();
                                    return {
                                        data: json
                                    }
                                })
                                .catch(error => { throw 'Data Update Error' });
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
}

function isNotEmpty(value: any): boolean {
    return value !== undefined && value !== null && value !== "";
}
