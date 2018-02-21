import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';
import { HttpModule }    from '@angular/http';

import './rxjs-extensions';
import { DashboardComponent} from './Components/dashboard.component';
import { CoursesComponent} from './Components/courses.component';
import { InstructorComponent} from './Components/instructor.component';
import { RoomComponent} from './Components/room.component';
import { RatingComponent} from './Components/rating.component';
import { ScheduleComponent} from './Components/schedule.component';
import { TrackComponent} from './Components/track.component';
import { AppComponent }         from './Components/app.component';
import { AppRoutingModule } from './app-routing.module';
import { CourseService }          from './Services/course.service';
import { InstructorService }          from './Services/instructor.service';
import { RoomService }          from './Services/room.service';
import { RatingService }          from './Services/rating.service';
import { ScheduleService }          from './Services/schedule.service';
import { TrackService }          from './Services/track.service';
import { Configuration } from './app.constants';
import {APP_BASE_HREF} from '@angular/common';
import {Ng2PaginationModule} from 'ng2-pagination';
import { AuthenticationService } from './Services/authentication.service';
import { LoginComponent } from './Components/login.component';
import { AuthGuard } from './Guards/auth.guard';

@NgModule({
  imports:      [ 
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    AppRoutingModule, 
    Ng2PaginationModule,
    ],
  declarations: [ AppComponent, DashboardComponent, CoursesComponent, InstructorComponent, RoomComponent, RatingComponent,ScheduleComponent, TrackComponent, LoginComponent ],
  providers: [ {provide: APP_BASE_HREF, useValue : '/' },
    CourseService, InstructorService, RoomService, RatingService, ScheduleService, TrackService, Configuration, AuthenticationService, AuthGuard
  ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
