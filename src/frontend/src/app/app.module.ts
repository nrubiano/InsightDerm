import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Router, NavigationStart, NavigationEnd, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { routing } from './app.routing';

import { AppComponent } from './app.component';
import { TopBarComponent} from './components/top-bar/top-bar.component';
import { MenuLeftComponent } from './components/menu-left/menu-left.component';
import { MenuRightComponent } from './components/menu-right/menu-right.component';
import { FooterComponent } from './components/footer/footer.component';

import { StructureModule } from './structure/structure.module';
import { FeaturesModule } from './features/features.module';

import { DoctorsService } from './services/doctors.services';

declare var NProgress: any;

@NgModule({
    declarations: [
        AppComponent,
        TopBarComponent,
        MenuLeftComponent,
        MenuRightComponent,
        FooterComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        HttpModule,
        RouterModule,
        StructureModule,
        FeaturesModule,        
        NgbModule.forRoot(),
        routing
    ],
    providers: [
      DoctorsService      
    ],
    bootstrap: [ AppComponent ]
})

export class AppModule {
  constructor(private router: Router) {
    router.events.subscribe((event) => {

      if(event instanceof NavigationStart) {
        NProgress.start();
      }

      if(event instanceof NavigationEnd) {
        setTimeout(function(){
          NProgress.done();
        }, 200);
      }

    });
  }
}
