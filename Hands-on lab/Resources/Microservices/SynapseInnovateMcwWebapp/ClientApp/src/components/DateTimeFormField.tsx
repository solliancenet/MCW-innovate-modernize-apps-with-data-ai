import React, {useState} from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";
import {DateUtils} from "../utils/DateUtils";

export const DateTimeFormField: React.FC<{name: string, value: Date | null, setValue: (v: Date | null) => void}> = ({name, value, setValue}) => {
	const [datePart, setDatePart] = useState<Date | null>(new Date());
	const [timePart, setTimePart] = useState<Date | null>(new Date());
	
	const handleDatePartChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		setDatePart(e.target.valueAsDate);
		constructAndSetDate();
	}
	
	const handleTimePartChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		setTimePart(e.target.valueAsDate);
		constructAndSetDate();
	}
	
	const constructAndSetDate = () => {
		const date = new Date();
		
		date.setFullYear(datePart?.getFullYear() ?? 0);
		date.setMonth(datePart?.getMonth() ?? 0);
		date.setDate(datePart?.getDate() ?? 0);

		date.setHours(timePart?.getHours() ?? 0);
		date.setMinutes(timePart?.getMinutes() ?? 0);
		date.setSeconds(timePart?.getSeconds() ?? 0);

		setValue(date);
	}
	
	return (
		<FormGroup row>
			<Label sm={2}>{name}</Label>
			<Col sm={10} md={6}>
				<Input type="date" placeholder="Date Part" value={DateUtils.formatDate(datePart)} onChange={e => handleDatePartChange(e)} />
				<Input type="time" placeholder="Time Part" value={DateUtils.formatTime(timePart)} onChange={e => handleTimePartChange(e)} />
			</Col>
		</FormGroup>
	)
}