import { Injectable } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import { HttpClient } from '@angular/common/http';

/**
 * Patients Service
 */
@Injectable()
export class PatientsService {
    store: CustomStore;

    constructor(private http: HttpClient) {
        this.setupStore();
    }
    /**
     * Setup the store with the http methods
     */
    setupStore() {
        const api = AppSettings.API + '/Patients'
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
                let params = '';

                if (loadOptions.skip) {
                    params += 'skip=' + loadOptions.skip;
                }

                if (loadOptions.take) {
                    params += '&take=' + loadOptions.take;
                }

                if (loadOptions.filter) {
                    params += '&$filter=' + loadOptions.filter;
                }

                if (loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if (loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }

                let query = '';
                if (params.length > 0) {
                    query = '?' + params;
                }

                return http.get<any[]>(api + query)
                    .toPromise()
                    .then(response => {
                        return {
                            data: response,
                            totalCount: response.length
                        }
                    })
                    .catch(error => { throw new Error('Data Loading Error') });
            },
            update: (entity, updatedValues): Promise<any> => {
                return http.put(api + '/' + encodeURIComponent(entity.id), {...entity, ...updatedValues})
                                .toPromise()
                                .then(response => {
                                    return {
                                        data: response
                                    }
                                })
                                .catch(error => { throw new Error('Data Update Error') });
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
            }
        });
    }
}
