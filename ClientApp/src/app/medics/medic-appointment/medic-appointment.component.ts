import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
  OnInit,
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours,
} from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView,
} from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { DoctorService } from 'src/shared/services/doctor.service';
import { ActivatedRoute } from '@angular/router';

const colors: Record<string, EventColor> = {
  yellow: {
    primary: '#ffd740',
    secondary: '#FDF1BA',
  },
  purple: {
    primary: '#673ab7',
    secondary: '#FAE3E3',
  },
};

@Component({
  selector: 'app-medic-appointment',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styles: [
    `
      h3 {
        margin: 0 0 10px;
      }

      pre {
        background-color: #f5f5f5;
        padding: 15px;
      }
    `,
  ],
  templateUrl: 'medic-appointment.component.html',
})
export class MedicAppointmentComponent implements OnInit {
  @ViewChild('modalContent', { static: true })
  doctorId: string = '';
  modalContent!: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();
  currentDate: Date = new Date();

  modalData:
    | {
        action: string;
        event: CalendarEvent;
      }
    | undefined;

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  refresh = new Subject<void>();

  events: CalendarEvent[] = [];

  activeDayIsOpen: boolean = true;

  constructor(
    private modal: NgbModal,
    private doctorService: DoctorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.parent?.params.subscribe(
      (params) => (this.doctorId = params['id'])
    );
    this.doctorService.getAppointments(this.doctorId).subscribe({
      next: (appointments) => {
        console.log(appointments);

        appointments.forEach((appointment, index) => {
          this.events.push({
            start: new Date(appointment.startTime),
            end: new Date(appointment.endTime),
            title: 'Appointment' + index,
            color: { ...colors['purple'] },
            actions: this.actions,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
            draggable: true,
          });
        });
      },
      error: (error) => console.log(error),
    });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  addEvent(): void {
    this.events = [
      ...this.events,
      {
        title: 'New event',
        start: startOfDay(new Date()),
        end: endOfDay(new Date()),
        color: colors['purple'],
        draggable: true,
        resizable: {
          beforeStart: true,
          afterEnd: true,
        },
      },
    ];
  }

  deleteEvent(eventToDelete: CalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
