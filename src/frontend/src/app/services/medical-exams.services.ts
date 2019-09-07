import { Injectable } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import { HttpClient } from '@angular/common/http';

/**
 * MedicalExams Service
 */
@Injectable()
export class MedicalExamsService {
    store: CustomStore;

    constructor(private http: HttpClient) {
        this.setupStore();
    }
    /**
     * Setup the store with the http methods
     */
    setupStore() {
        const api = AppSettings.API + '/medicallaboratorytypes';
        const http = this.http;
        this.store = new CustomStore({
            insert: (item): Promise<any> => {
                return  http
                        .post(api, item)
                        .toPromise()
                        .then(response => {
                            return response;
                        })
                        .catch(error => {
                            throw error._body;
                        });

            },
            load: (loadOptions): Promise<any> => {
                let params = '?';

                params += 'skip=' + loadOptions.skip || 0;
                params += '&take=' + loadOptions.take || 12;

                if (loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if (loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }

                return http.get<any[]>(api + params)
                    .toPromise()
                    .then(response => {
                        return {
                            data: response,
                            totalCount: response.length
                        }
                    })
                    .catch(() => { throw new Error('Data Loading Error') });
            },
            update: (entity, updatedValues): Promise<any> => {
                return http.put(api + '/' + encodeURIComponent(entity.id), {...entity, ...updatedValues})
                                .toPromise()
                                .then(response => {
                                    return {
                                        data: response
                                    }
                                })
                                .catch(() => { throw new Error('Data Update Error') });
            },
            remove: (key): Promise<any> => {
                return http.delete(api + '/' + encodeURIComponent(key.id))
                            .toPromise()
                            .then(response => {
                                return {
                                    data: response
                                }
                            })
                            .catch(error => {
                                console.log(error);
                                throw new Error('Data Update Error')
                            });
            },
            byKey: (key: any): Promise<any> => {
                return http.get(api + '/' + key)
                    .toPromise()
                    .then(response => {
                        return response;
                    })
                    .catch(() => { throw new Error('Data Loading Error') });
            }
        });
    }
}
