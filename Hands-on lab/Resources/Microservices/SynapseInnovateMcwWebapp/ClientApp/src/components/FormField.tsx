import React from "react";
import {Col, FormGroup, Input, Label} from "reactstrap";

export const FormField: React.FC<{ name: string, value: any, setValue: (v: any) => void }> = ({name, value, setValue}) => {
	const type = typeof value === "number"
		? "number"
		: "text";

	return (
		<FormGroup row>
			<Label sm={2}>{name}</Label>
			<Col sm={10} md={6}>
				<Input type={type} placeholder={name} value={value} onChange={e => {
					typeof value === "number"
						? setValue(+e.target.value)
						: setValue(e.target.value)
				}} />
			</Col>
		</FormGroup>
	)
}