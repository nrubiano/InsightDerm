import { Inject, Injectable } from '@angular/core';
import { Http, HttpModule } from '@angular/http';
import CustomStore from 'devextreme/data/custom_store';
import { AppSettings } from '../app.config';

/**
 * Doctors Service
 */
@Injectable()
export class DoctorsService
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
        var api = AppSettings.API + "/Doctors"
        var http = this.http;
        this.store = new CustomStore({
            insert: (item) : Promise<any> => 
            {
                return  http
                        .post(api, item)
                        .toPromise()
                        .then(response => {
                            return response;
                        })
                        .catch(error => { 
                            console.log(error);
                            throw error._body.replace(/["]+/g, '');
                        });
                
            },
            load: (loadOptions) :Promise<any> =>
            {
                var params = '?';

                params += 'skip=' + loadOptions.skip || 0;
                params += '&take=' + loadOptions.take || 12;

                if(loadOptions.sort) {
                    params += '&orderby=' + loadOptions.sort[0].selector;
                    if(loadOptions.sort[0].desc) {
                        params += ' desc';
                    }
                }
                return http.get(api + params)
                    .toPromise()
                    .then(response => {
                        var json = response.json();
                        
                        return {
                            data: json,
                            totalCount: json.length
                        }
                    })
                    .catch(error => { throw 'Data Loading Error' });
            },
            update: (entity, updatedValues):Promise<any> => {                
                return http.put(api + "/" + encodeURIComponent(entity.id), Object.assign(entity, updatedValues))
                                .toPromise()
                                .then(response => {
                                    var json = response.json();
                                    return {
                                        data: json
                                    }
                                })
                                .catch(error => { throw 'Data Update Error' });
            },
            remove: (entity):Promise<any> => {                
                return http.delete(api + "/" + encodeURIComponent(entity.id), entity)
                                .toPromise()
                                .then(response => {
                                    var json = response.json();
                                    return {
                                        data: json
                                    }
                                })
                                .catch(error => { throw 'Data Update Error' });
            }
        });
    }
}