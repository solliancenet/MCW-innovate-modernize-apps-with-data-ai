import React from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";
import {DateUtils} from "../utils/DateUtils";

export const DateFormField: React.FC<{ name: string, value: Date | null, setValue: (v: Date | null) => void }> = ({name, value, setValue}) => {
	return (
		<FormGroup row>
			<Label sm={2}>{name}</Label>
			<Col sm={10} md={6}>
				<Input type="date" placeholder={name} value={DateUtils.formatDate(value)} onChange={e => {
					console.log("Date value changed:", e.target.value);
					setValue(e.target.valueAsDate);
				}} />
			</Col>
		</FormGroup>
	)
}