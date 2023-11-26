import React, { useState } from 'react';

function LineupPositionModal({ isOpen, onClose, onSubmit }) {
    const [selectedPosition, setSelectedPosition] = useState('');

    const handleSubmit = () => {
        onSubmit(selectedPosition);
        onClose();
    }

    return (
        <div style={{ display: isOpen ? 'block' : 'none' }}>
            <div>
                <select onChange={e => setSelectedPosition(e.target.value)} value="selectedPosition">
                    <option value="QB1">QB1</option>
                    <option value="QB2">QB2</option>
                    <option value="RB1">RB1</option>
                    <option value="RB2">RB2</option>
                    <option value="WR1">WR1</option>
                    <option value="WR2">WR2</option>
                    <option value="WR3">WR3</option>
                    <option value="TE1">TE1</option>
                    <option value="FLEX">FLEX</option>
                    <option value="K">K</option>
                    <option value="DEF">DEF</option>
                </select>
            </div>
            <button onClick={handleSubmit}>Submit</button>
            <button onClick={onClose}>Cancel</button>
        </div>
    );
}

export default LineupPositionModal;