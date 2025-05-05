import React from "react";
import FamilyMemberCard from "./FamilyMemberCard";

function FamilyMembersList({ members }) {
    return (
        <div>
            {members.length === 0 ? (
                <p>Нет зарегистрированных участников.</p>
            ) : (
                members.map(member => (
                    <FamilyMemberCard key={member.id} member={member} />
                ))
            )}
        </div>
    );
}


export default FamilyMembersList;
