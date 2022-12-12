import { forwardRef } from 'react';
import { useState } from 'react';
import DatePicker from 'react-datepicker';
import { BsFillCalendarDateFill } from 'react-icons/bs';

export const DatePickerCustom = ({ date, onChangeDate, name = '', classNameInValid = false }) => {
    return (
        <div className={`datepicker`}>
            <label>
                <DatePicker
                    name={name}
                    selected={date}
                    className={`${classNameInValid ? 'border-danger' : ''} form-control w-full `}
                    onChange={(date) => onChangeDate(date)}
                    placeholderText="dd/MM/yyyy"
                    dateFormat="dd/MM/yyyy"
                    shouldCloseOnSelect={true}
                />
                <span className="d-flex align-center icon-container">
                    <BsFillCalendarDateFill />
                </span>
            </label>
        </div>
    );
};
