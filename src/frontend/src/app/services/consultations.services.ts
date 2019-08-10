import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';
import {Observable} from "rxjs";

/**
 * Consultations Service
 */
@Injectable()
export class ConsultationsService
{
    store : CustomStore;

    constructor(private http: Http)
    {
        this.setupStore();
    }
    /**
     * Setup the store with the http methods
     */
    setupStore()
    {
        let api = AppSettings.API + "/consultations";
        let http = this.http;
        this.store = new CustomStore({
            byKey: (key) : Promise<any> =>
            {
                return http.get(`${api}/${key}`)
                    .toPromise()
                    .then(response => response.json())
                    .catch(error => { throw 'Data Loading Error' });
            },
            insert: (item) : Promise<any> =>
            {
                return  http
                    .post(api, item)
                    .toPromise();
            },
            load: (loadOptions): Promise<any> =>
            {
                let params = '';

                if(loadOptions.skip){
                    params += 'skip=' + loadOptions.skip;
                }

                if(loadOptions.take){
                    params += '&take=' + loadOptions.take;
                }

                if(loadOptions.filter) {
                    params += '&$filter=' + loadOptions.filter;
                }

                if(loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if(loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }

                let query = '';
                if(params.length > 0){
                    query = '?' + params;
                }

                return http.get(api + query)
                    .toPromise()
                    .then(response => {
                        let json = response.json();

                        return {
                            data: json,
                            totalCount: json.length
                        }
                    })
                    .catch(error => { throw 'Data Loading Error' });
            },
            update: (entity, updatedValues):Promise<any> => {
                return http.put(api + "/" + encodeURIComponent(entity.id), updatedValues)
                    .toPromise()
                    .then(response => {
                        let json = response.json();
                        return {
                            data: json
                        }
                    })
                    .catch(error => { throw 'Data Update Error' });
            },
            remove: (key) : Promise<any> => {
                return http.delete(api + "/" + encodeURIComponent(key.id))
                    .toPromise()
                    .then(response => {
                        let json = response.json();
                        return {
                            data: json
                        }
                    })
                    .catch(error => {
                        console.log(error);
                        throw 'Data Update Error'
                    });
            }
        });
    }
}
